﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.DataAccess
{
    public class RecipeDAO
    {
        private static RecipeDAO instance = null;
        private static readonly object instanceLock = new object();

        public static RecipeDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RecipeDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Recipe> GetRecipes()
        {
            var list = new List<Recipe>();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    list = context.Recipes.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving recipes list: " + ex.Message);
            }
            return list;
        }

		public List<Recipe> GetRecipeListById(int id)
		{
			var list = new List<Recipe>();
			try
			{
				using (var context = new CookingWebsiteContext())
				{
					list = context.Recipes.Include(s => s.CreatorNavigation).ToList();
					list = list.Where(x => x.RecipeId.Equals(id)).ToList();

				}
			}
			catch (Exception ex)
			{

				throw new Exception("Error retrieving recipes list: " + ex.Message);
			}
			return list;
		}

        public List<Recipe> GetRecipeListByUserId(int userId)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    // Truy vấn cơ sở dữ liệu để lấy danh sách công thức theo userId
                    var list = context.Recipes
                        .Where(x => x.Creator == userId)
                        .ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving recipes list by userId: " + ex.Message);
            }
        }



        public Recipe GetRecipeById(int id)
        {
			Recipe recipe = new Recipe();
			try
			{
				using (var context = new CookingWebsiteContext())
				{
					recipe = context.Recipes.FirstOrDefault(x => x.RecipeId == id);
				}
				if (recipe == null)
				{
					throw new Exception("Recipe doesn't exist");
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return recipe;
		}

        public void SaveRecipe(Recipe recipe)
        {
			try
			{
				using (var context = new CookingWebsiteContext())
				{
					var existingRecipe = context.Recipes.FirstOrDefault(x => x.RecipeId == recipe.RecipeId);
					if (existingRecipe != null)
					{
						throw new Exception("Recipe already exists");
					}

					context.Recipes.Add(recipe);
					context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

        public void UpdateRecipe(Recipe recipe)
        {
			try
			{
				using (var context = new CookingWebsiteContext())
				{
					var existingRecipe = context.Recipes.FirstOrDefault(x => x.RecipeId == recipe.RecipeId);

					if (existingRecipe != null)
					{
						context.Entry(existingRecipe).CurrentValues.SetValues(recipe);
						context.SaveChanges();
					}
					else
					{
						throw new Exception("Recipe not found");
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public void DeleteRecipe(Recipe recipe)
		{
			try
			{
				using (var context = new CookingWebsiteContext())
				{
					var recipeToDelete = context.Recipes.FirstOrDefault(x => x.RecipeId == recipe.RecipeId);
					if (recipeToDelete == null)
					{
						throw new Exception("Recipe is null");
					}
					else
					{
						context.Recipes.Remove(recipeToDelete);
						context.SaveChanges();
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
