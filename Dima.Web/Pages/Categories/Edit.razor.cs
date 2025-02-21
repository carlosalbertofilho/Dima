using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Categories;

public partial class EditCategoryPage : ComponentBase
{
    #region Properties

    protected bool IsBusy { get; set; } = false;
    protected bool IsEditBusy { get; set; } = false;
    protected UpdateCategoryRequest InputModel { get; set; } = null!;

    #endregion

    #region Parameters

    [Parameter] public string Id { get; set; } = string.Empty;

    #endregion

    #region Services

    [Inject] protected ISnackbar Snackbar { get; set; } = null!;
    [Inject] protected ICategoryHandler Handler { get; set; } = null!;
    [Inject] protected NavigationManager NavigationManager { get; set; } = null!;

    #endregion

    #region Override

    protected override async Task OnInitializedAsync() 
    {
        IsBusy = true;
        try
        {
            var request = new GetCategoryByIdRequest
            {
                Id = long.Parse(Id)
            };
            var response = await Handler.GetByIdAsync(request);

            if (response is { IsSuccess: true, Data: not null })
            {
                InputModel = new UpdateCategoryRequest
                {
                    Id = response.Data.Id,
                    Title = response.Data.Title,
                    Description = response.Data.Description,
                    UserId = response.Data.UserId,
                };
            }
            else
            {
                Snackbar.Add(response.Message!, Severity.Error);
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add(
                ex is not ArgumentNullException and not OverflowException and not FormatException
                    ? "Parâmetro inválido: formato incorreto!"
                    : ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion

    #region Methods

    protected async Task OnValidSubmitAsync()
    {
        IsEditBusy = true;
        try
        {
            var result = await Handler.UpdateAsync(InputModel);
            if (result.IsSuccess)
            {
                Snackbar.Add(result.Message!, Severity.Success);
                NavigationManager.NavigateTo("/categorias");
            }
        } catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsEditBusy = false;
        }
    }

    #endregion
}