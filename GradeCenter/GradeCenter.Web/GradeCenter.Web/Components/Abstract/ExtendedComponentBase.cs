namespace GradeCenter.Web.Components.Abstract
{
    public class ExtendedComponentBase<T> : ComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    }
}
