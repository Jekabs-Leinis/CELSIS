namespace CELSIS.Api.Models
{
    public class SelectRouteModel
    {
        public string GoogleRouteHash { get; set; }

        public List<SelectRoutePlaceModel> Places { get; set; }
    }
}
