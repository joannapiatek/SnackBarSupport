using Models.ViewModels;

namespace Models.Dto
{
    public class IngredientDto : DtoBase
    {
        public string Name { get; set; }

        public IngredientDto() {}

        public IngredientDto(IngredientSelect ingredient)
        {
            Id = ingredient.Id;
            Name = ingredient.Name;
        }

        public override bool Equals(System.Object obj)
        {
            IngredientDto p = obj as IngredientDto;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Id == p.Id);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;

                hash = hash * 23 + Id.GetHashCode();
                return hash;
            }
        }
    }
}
