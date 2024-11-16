//-------------------------------------------------------------------------
// <copyright file="Recipe.cs" company="Universidad Católica del Uruguay">
// Copyright (c) Programación II. Derechos reservados.
// </copyright>
//-------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Full_GRASP_And_SOLID
{
    public class Recipe : IRecipeContent // Modificado por DIP
    {
        // Cambiado por OCP
        private IList<BaseStep> steps = new List<BaseStep>();

        public Product FinalProduct { get; set; }

        // Propiedad Cooked de solo lectura
        public bool Cooked { get; private set; } = false;

        // Agregado por Creator
        public void AddStep(Product input, double quantity, Equipment equipment, int time)
        {
            Step step = new Step(input, quantity, equipment, time);
            this.steps.Add(step);
        }

        // Agregado por OCP y Creator
        public void AddStep(string description, int time)
        {
            WaitStep step = new WaitStep(description, time);
            this.steps.Add(step);
        }

        public void RemoveStep(BaseStep step)
        {
            this.steps.Remove(step);
        }

        // Agregado por SRP
        public string GetTextToPrint()
        {
            string result = $"Receta de {this.FinalProduct.Description}:\n";
            foreach (BaseStep step in this.steps)
            {
                result = result + step.GetTextToPrint() + "\n";
            }

            // Agregado por Expert
            result = result + $"Costo de producción: {this.GetProductionCost()}";

            return result;
        }

        // Agregado por Expert
        public double GetProductionCost()
        {
            double result = 0;

            foreach (BaseStep step in this.steps)
            {
                result = result + step.GetStepCost();
            }

            return result;
        }

        // Nuevo método para obtener el tiempo total de cocción
        public int GetCookTime()
        {
            int totalTime = 0;
            foreach (BaseStep step in this.steps)
            {
                totalTime += step.Time;
            }
            return totalTime;
        }

        // Método para iniciar la cocción
        public void Cook()
        {
            // Crear el adaptador pasando la instancia actual de Recipe
            TimerClient adapter = new RecipeTimerClientAdapter(this);
            CountdownTimer timer = new CountdownTimer();
            timer.Register(this.GetCookTime(), adapter);
        }

        // Método interno para actualizar la propiedad Cooked
        internal void SetCooked(bool cooked)
        {
            this.Cooked = cooked;
        }
    }
}