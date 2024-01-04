using ProductIdeas.Data;

namespace ProductIdeas.App.Pages.Products;

public class FoodTruckModel(LeadCaptureDatabase database) : ProductPageBase(database)
{
	protected override int ApplicationId => 1;

	public void OnGet()
	{
	}
}
