using ReservAntes.Servicios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;


namespace ReservAntes.Controllers
{

    //  [Authorize]
    [RoutePrefix("api/GoogleMapsHelper")]
    public class GoogleMapsHelperController : ApiController
    {

        [Route("GetLatitudeLongitudeFromAddress")]
        [Route("GetLatitudeLongitudeFromAddress/{address}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetLatitudeLongitudeFromAddress(string address)
        {
            var googleHelper = new GoogleHelper();

            var dto = googleHelper.CallMapsApi(new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("address", address)
            }, ModelState);

            if (ModelState.IsValid)
                return Ok(dto);

            return BadRequest(ModelState);

        }

        [Route("GetAddressFromLatitudeLongitude")]
        [Route("GetAddressFromLatitudeLongitude/{latlng}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAddressFromLatitudeLongitude(string latlng)
        {
            var googleHelper = new GoogleHelper();
            var dto = await googleHelper.CallMapsApi(new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("latlng", latlng)
            }, ModelState);

            if (ModelState.IsValid)
                return Ok(dto);

            return BadRequest(ModelState);
        }
    }
}
