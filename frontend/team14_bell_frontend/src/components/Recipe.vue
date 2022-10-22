<template>
  <v-container>
    <h2>Best Recipes</h2>
    <v-card
      class="mx-auto my-12"
      max-width="374"
      v-for="recipe in recipes"
      :key="recipe.id"
    >
      <v-img
        height="250"
        src="https://cdn.vuetifyjs.com/images/cards/cooking.png"
      ></v-img>

      <v-card-title>{{ recipe.name }}</v-card-title>

      <v-card-text>
        <div class="my-4 text-subtitle-1">Ingredients</div>
        <div>
          <li
            v-for="ingredient in recipe.ingredientWithQuantities"
            :key="ingredient"
          >
            {{ ingredient }}
          </li>
        </div>
        <div class="my-4">Preparation</div>
        <div>
          <li v-for="step in recipe.steps" :key="step">{{ step }}</li>
        </div>
      </v-card-text>

      <v-divider class="mx-4"></v-divider>

      <v-card-title>Perfect fit:</v-card-title>

      <v-card-text>
        <v-chip-group column>
          <v-chip>Bell Salami</v-chip>

          <v-chip class="deep-purple accent-4 white--text"
            >Bell Cervelat</v-chip
          >
          <v-chip>Bell Steak</v-chip>
        </v-chip-group>
      </v-card-text>
    </v-card>
  </v-container>
</template>

<script>
export default {
  name: "Recipe",

  data: () => ({
    recipes: [
      {
        id: 1,
        name: "Grilled Cheese",
        ingredientWithQuantities: [
          "1 teaspoon of salt",
          "chicken",
          "tomato sauce",
        ],
        steps: ["Step 1", "Step 2", "Step 3"],
      },
      {
        id: 2,
        name: "Grilled Cheese 2",
        ingredientWithQuantities: [
          "1 teaspoon of pepper",
          "fresh chicken",
          "tomato sauce extra",
        ],
        steps: ["Step 1", "Step 2", "Step 3"],
      },
    ],
  }),
};
</script>
