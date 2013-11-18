using SaveMySpot.Models;
using SaveMySpot.Services.Models;
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
        private const int TitleMinLenght = 5;
        private const int TitleMaxLenght = 35;
        private const String TitleAvailableSymbols = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890_-.!@$? ";

        [HttpGet]
        public IQueryable<SpotModel> GetSpots(string authCode)
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(
                () =>
                {
                    ValidateAuthCode(authCode);

                    var spots = from spot in this.Data.Spots.All()
                                where spot.AuthCode == authCode
                                select new SpotModel
                                {
                                    AuthCode = spot.AuthCode,
                                    Longitude = spot.Longitude,
                                    Latitude = spot.Latitude,
                                    Title = spot.Title 
                                };

                    return spots;
                });

            return responseMsg;
        }

        [HttpPost]
        public HttpResponseMessage PostSpot(SpotModel model)
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(
                () =>
                {
                    if (model == null)
                    {
                        throw new ArgumentOutOfRangeException(string.Format("Invalid input data!"));
                    }
                     
                    ValidateModel(model);

                    var spot = new Spot()
                    {
                        Title = model.Title,
                        Latitude = model.Latitude,
                        Longitude = model.Longitude,
                        AuthCode = model.AuthCode

                    };

                    this.Data.Spots.Add(spot);

                    this.Data.SaveChanges();

                    var responce = this.Request.CreateResponse(HttpStatusCode.OK);

                    return responce;

                });

            return responseMsg;
        }

        private void ValidateModel(SpotModel model)
        {
            ValidateAuthCode(model.AuthCode);
            ValidateTitle(model.Title); 
        } 

        private void ValidateTitle(string title)
        {
            if (title.Length < TitleMinLenght || title.Length > TitleMaxLenght)
            {
                throw new ArgumentOutOfRangeException(string.Format("Title lenght must be between {0} and {1} characters!", TitleMinLenght, TitleMaxLenght));
            }

            else if (title.Any(ch => !TitleAvailableSymbols.Contains(ch)))
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
