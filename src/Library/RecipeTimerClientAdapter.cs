namespace Full_GRASP_And_SOLID
{
    // Clase adaptador que implementa TimerClient
    public class RecipeTimerClientAdapter : TimerClient
    {
        private Recipe recipe;

        public RecipeTimerClientAdapter(Recipe recipe)
        {
            this.recipe = recipe;
        }

        public void TimeOut()
        {
            // Actualiza la propiedad Cooked de la receta
            this.recipe.SetCooked(true);
        }
    }
}