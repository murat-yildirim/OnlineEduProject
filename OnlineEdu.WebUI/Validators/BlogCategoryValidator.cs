using FluentValidation;
using OnlineEdu.WebUI.DTOs.BlogCategoryDtos;

namespace OnlineEdu.WebUI.Validators
{
    public class BlogCategoryValidator : AbstractValidator<CreateBlogCategoryDto>
    {

        public BlogCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Blog Kategori Adı Boş Geçilemez")
                .MaximumLength(30).WithMessage("Blog Kategori Adı En Fazla 30 Karakter Olmalıdır");
        }

    }
}
