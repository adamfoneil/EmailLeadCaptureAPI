using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductIdeas.Data;

namespace ProductIdeas.App.Pages.Products;

public abstract class ProductPageBase(LeadCaptureDatabase database) : PageModel
{
    protected readonly LeadCaptureDatabase Database = database;

    protected abstract int ApplicationId { get; }

    [BindProperty]
    public string Email { get; set; }

    public async Task OnPostEmailSubmitAsync()
    {
        await Database.SaveLeadAsync(ApplicationId, Email);
    }
}
