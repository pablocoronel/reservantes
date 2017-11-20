using CapaServicio.Dto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using System.Xml.Linq;
using System.Web.Http;
using System.Web.Helpers;
using ModelStateDictionary = System.Web.Http.ModelBinding.ModelStateDictionary;
namespace ReservAntes.Servicios
{
    public class GoogleHelper
    {

        private readonly HttpClient _client = new HttpClient { BaseAddress = new Uri("https://maps.googleapis.com/maps/api/geocode/xml") };

        public async Task<GoogleMapsDto> CallMapsApi(List<KeyValuePair<string, object>> parameters, ModelStateDictionary modelstate, bool withKey = true)
        {

            if (withKey)
                parameters.Add(new KeyValuePair<string, object>("key", ConfigurationManager.AppSettings["GoogleMapsKey"]));

            var request = await _client.GetAsync(parameters.AsQueryString()).ConfigureAwait(continueOnCapturedContext: false);
            //.ConfigureAwait(continueOnCapturedContext: false)

            if (request.IsSuccessStatusCode)
            {
                var dto = new GoogleMapsDto();
                var xdoc = XDocument.Load(await request.Content.ReadAsStreamAsync());
                switch (xdoc.Element("GeocodeResponse")?.Element("status")?.Value)
                {
                    case "OK":
                        var result = xdoc.Element("GeocodeResponse")?.Element("result");
                        dto.Status = xdoc.Element("GeocodeResponse")?.Element("status")?.Value;
                        dto.Latitude = result?.Element("geometry")?.Element("location")?.Element("lat")?.Value;
                        dto.Longitude = result?.Element("geometry")?.Element("location")?.Element("lng")?.Value;
                        dto.Address = result?.Element("formatted_address")?.Value;
                        foreach (var element in result.Elements())
                        {
                            if (element.Name != "address_component") continue;

                            if (element?.Element("type")?.Value == "postal_code")
                                dto.PostalCode =
                                    !string.IsNullOrWhiteSpace(element?.Element("long_name")?.Value)
                                        ? element?.Element("long_name")?.Value
                                        : element?.Element("short_name")?.Value;

                            if (element?.Element("type")?.Value == "route")
                                dto.StreetName =
                                    !string.IsNullOrWhiteSpace(element?.Element("long_name")?.Value)
                                        ? element?.Element("long_name")?.Value
                                        : element?.Element("short_name")?.Value;
                        }

                        break;
                    case "ZERO_RESULTS":
                        dto.Status = "ZERO_RESULTS";
                        break;
                    default:
                        modelstate.AddModelError("status", xdoc.Element("GeocodeResponse")?.Element("status")?.Value);
                        modelstate.AddModelError("GeocodeResponse", xdoc.Element("GeocodeResponse")?.Element("error_message")?.Value);
                        break;
                }

                return dto;
            }

            modelstate.AddModelError("request", "Solicitud no válida");

            return null;
        }

        public async Task<GoogleMapsDto> CallMapsApi(List<KeyValuePair<string, object>> parameters, bool withKey = true)
        {

            if (withKey)
                parameters.Add(new KeyValuePair<string, object>("key", ConfigurationManager.AppSettings["GoogleMapsKey"]));

            var request = await _client.GetAsync(parameters.AsQueryString()).ConfigureAwait(continueOnCapturedContext: false);

            if (request.IsSuccessStatusCode)
            {
                var dto = new GoogleMapsDto();
                var xdoc = XDocument.Load(await request.Content.ReadAsStreamAsync());
                switch (xdoc.Element("GeocodeResponse")?.Element("status")?.Value)
                {
                    case "OK":
                        var result = xdoc.Element("GeocodeResponse")?.Element("result");
                        dto.Status = xdoc.Element("GeocodeResponse")?.Element("status")?.Value;
                        dto.Latitude = result?.Element("geometry")?.Element("location")?.Element("lat")?.Value;
                        dto.Longitude = result?.Element("geometry")?.Element("location")?.Element("lng")?.Value;
                        dto.Address = result?.Element("formatted_address")?.Value;
                        foreach (var element in result.Elements())
                        {
                            if (element.Name != "address_component") continue;

                            if (element?.Element("type")?.Value == "postal_code")
                                dto.PostalCode =
                                    !string.IsNullOrWhiteSpace(element?.Element("long_name")?.Value)
                                        ? element?.Element("long_name")?.Value
                                        : element?.Element("short_name")?.Value;

                            if (element?.Element("type")?.Value == "route")
                                dto.StreetName =
                                    !string.IsNullOrWhiteSpace(element?.Element("long_name")?.Value)
                                        ? element?.Element("long_name")?.Value
                                        : element?.Element("short_name")?.Value;
                        }

                        break;
                    case "ZERO_RESULTS":
                        dto.Status = "ZERO_RESULTS";
                        break;

                }

                return dto;
            }

            return null;
        }
    }


}