using SaveMySpot.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SaveMySpot.Services.Controllers
{
    public class SpotsController : BaseController
    {
        private const int Sha1Length = 40;
        private const int titleMinLenght = 5;
        private const int titleMaxLenght = 35;
        private const String titleAvailableSymbols = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890_-.!@$? ";

        [HttpGet]
        public IQueryable<Spot> GetSpots(string authCode)
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(
                () =>
                {
                    ValidateAuthCode(authCode);

                    var spots = this.Data.Spots.All().Where(s => s.AuthCode == authCode);

                    return spots;
                });

            return responseMsg;
        }

        [HttpPost]
        public HttpResponseMessage PostSpot(Spot model)
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(
                () =>
                {
                    if (model == null)
                    {
                        throw new ArgumentOutOfRangeException(string.Format("Invalid input data!"));
                    }
                     
                    ValidateModel(model);

                    this.Data.Spots.Add(model);

                    this.Data.SaveChanges();

                    var responce = this.Request.CreateResponse(HttpStatusCode.OK);

                    return responce;

                });

            return responseMsg;
        }

        private void ValidateModel(Spot model)
        {
            ValidateAuthCode(model.AuthCode);
            ValidateTitle(model.Title); 
        } 

        private void ValidateTitle(string title)
        {
            if (title.Length < titleMinLenght || title.Length > titleMaxLenght)
            {
                throw new ArgumentOutOfRangeException(string.Format("Title lenght must be between {0} and {1} characters!", titleMinLenght, titleMaxLenght));
            }

            else if (title.Any(ch => !titleAvailableSymbols.Contains(ch)))
            {
                throw new ArgumentOutOfRangeException(string.Format("Invalid character in title. Please try again!"));
            }
        }

        private void ValidateAuthCode(string authCode)
        {
            if (authCode == null)
            {
                throw new ArgumentNullException("Authentication code cannot be null(empty)!");
            }
            else if (authCode.Length != Sha1Length)
            {
                throw new ArgumentOutOfRangeException("Authentication code is not valid");
            }
        }
    }
}
