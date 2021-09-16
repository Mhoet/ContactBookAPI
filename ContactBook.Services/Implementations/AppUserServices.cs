using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ContactBook.Common;
using ContactBook.DTO;
using ContactBook.DTO.Mappings;
using ContactBook.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ContactBook.Services
{
    public class AppUserServices : IAppUserServices
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IOptions<PhotoUploadSettings> _photoUploadSettings;
        private readonly Cloudinary cloudinary;

        public AppUserServices(UserManager<AppUser> userManager, IConfiguration configuration, IOptions<UserPhotoSettings> pictureSettings, IOptions<PhotoUploadSettings> photoUploadSettings)
        {
            _userManager = userManager;
            _configuration = configuration;
            _photoUploadSettings = photoUploadSettings;
            cloudinary = new Cloudinary(new Account(pictureSettings.Value.AccountName, pictureSettings.Value.ApiKey, pictureSettings.Value.ApiSecret));
        }
        public async Task<ResponseDTO> AddUser(RegistrationDTO registration)
        {
            AppUser appUser = AppUserMapping.GetAppUser(registration);
            IdentityResult result = await _userManager.CreateAsync(appUser, registration.Password);
            await _userManager.AddToRoleAsync(appUser, "Regular");
            if (result.Succeeded)
            {
                return AppUserMapping.GetResponse(appUser);
            }
            string errors = string.Empty;

            foreach (var error in result.Errors)
            {
                errors += error.Description + Environment.NewLine;
            }
            throw new MissingFieldException(errors);
        }

        public async Task<bool> DeleteAppUser(string userId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId);
            if (appUser == null)
            {
                throw new ArgumentException("Resource not found");
            }
            var result = await _userManager.DeleteAsync(appUser);
            if (result.Succeeded)
            {
                return true;
            }
            string errors = string.Empty;
            foreach (var error in result.Errors)
            {
                errors += error.Description + Environment.NewLine;
            }
            throw new MissingMemberException(errors);
        }

        public async Task<ResponseDTO> GetUserByEmail(string email)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(email);
            if (appUser == null)
            {
                throw new ArgumentException("Resource not found");
            }
            return AppUserMapping.GetResponse(appUser);
        }

        public async Task<ResponseDTO> GetUserById(string userId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId);
            if (appUser == null)
            {
                throw new ArgumentException("Resource not found");
            }
            return AppUserMapping.GetResponse(appUser);
        }

        public Paging<ResponseDTO> GetUsers(PagingDTO paging)
        {
            var users = _userManager.Users;
            if (users == null)
            {
                throw new ArgumentException("No record");
            }
            List<ResponseDTO> appUsers = new List<ResponseDTO>();
            foreach (AppUser user in users)
            {
                appUsers.Add(AppUserMapping.GetResponse(user));
            }
            var pages = Paging<ResponseDTO>.CreatePages(appUsers, paging.PageNumber, paging.UsersPerPage);
            return pages;
        }

        public Paging<ResponseDTO> SearchUsers(string searchQuery, PagingDTO paging)
        {
            string searchTerm = searchQuery.ToLower();
            var users = _userManager.Users.Where(x => x.FirstName.ToLower().Contains(searchTerm)
                || x.LastName.ToLower().Contains(searchTerm)
                || x.Email.ToLower().Contains(searchTerm)).ToList();
            if (users == null)
            {
                throw new ArgumentException("Invalid records");
            }
            List<ResponseDTO> appUsers = new List<ResponseDTO>();
            foreach (AppUser user in users)
            {
                appUsers.Add(AppUserMapping.GetResponse(user));
            }

            var pages = Paging<ResponseDTO>.CreatePages(appUsers, paging.PageNumber, paging.UsersPerPage);
            return pages;
        }

        public async Task<bool> UpdateAppUser(string userId, UpdateDTO userUpdateDTO)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId);
            if (appUser == null)
            {
                throw new ArgumentException("Resource not found");
            }

            appUser.FirstName = string.IsNullOrWhiteSpace(userUpdateDTO.FirstName) ? appUser.FirstName : userUpdateDTO.FirstName;
            appUser.LastName = string.IsNullOrWhiteSpace(userUpdateDTO.LastName) ? appUser.LastName : userUpdateDTO.LastName;
            appUser.PhoneNumber = string.IsNullOrWhiteSpace(userUpdateDTO.PhoneNumber) ? appUser.PhoneNumber : userUpdateDTO.PhoneNumber;

            var result = await _userManager.UpdateAsync(appUser);
            if (result.Succeeded)
            {
                return true;
            }
            string errors = string.Empty;
            foreach (var error in result.Errors)
            {
                errors += error.Description + Environment.NewLine;
            }
            throw new MissingMemberException(errors);
        }

        public async Task<bool> UpdatePhoto(string userId, PhotoDTO picture)
        {
            try
            {
                var userImage = await UploadPhoto(picture);
                AppUser appUser = await _userManager.FindByIdAsync(userId);
                if (appUser == null)
                {
                    throw new ArgumentException("Resource not found");
                }
                appUser.PhotoUrl = userImage.Url.ToString();
                var result = await _userManager.UpdateAsync(appUser);
                if (result.Succeeded)
                {
                    return true;
                }
                string errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += error.Description + Environment.NewLine;
                }
                throw new MissingMemberException(errors);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<UploadResult> UploadPhoto(PhotoDTO picture)
        {
            bool photoFormat = false;
            var listOfPhotoExtensions = _photoUploadSettings.Value.Formats;
            foreach (var item in listOfPhotoExtensions)
            {
                if (picture.PhotoUrl.FileName.EndsWith(item))
                {
                    photoFormat = true;
                    break;
                }
            }

            if (!photoFormat)
            {
                throw new ArgumentException("Invalid File Format");
            }

            var uploadResult = new ImageUploadResult();

            using (var pictureStream = picture.PhotoUrl.OpenReadStream())
            {
                string fileName = Guid.NewGuid().ToString() + picture.PhotoUrl.FileName;
                uploadResult = await cloudinary.UploadAsync(new ImageUploadParams()
                {
                    File = new FileDescription(fileName, pictureStream),
                    Transformation = new Transformation().Crop("thumb").Gravity("face").Width(150).Height(150)
                });
            }
            return uploadResult;
        }
    }
}
