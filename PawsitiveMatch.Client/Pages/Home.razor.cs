using PawsitiveMatch.SharedModels.Models;

namespace PawsitiveMatch.Client.Pages
{
    public partial class Home
    {
        public void NavToPetType(PetType pet)
        {
            Nav.NavigateTo($"/pets/{pet}");
        }
    }
}