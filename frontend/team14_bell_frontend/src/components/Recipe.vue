<template>
  <v-container>
    <h2>Best Recipes</h2>
    <v-card
      class="mx-auto my-12"
      max-width="374"
      v-for="recipe in recipes"
      :key="recipe.id"
    >
      <v-skeleton-loader
        class="mx-auto"
        heigth="250"
        type="card"
      ></v-skeleton-loader>

      <v-card-title>{{ recipe.Name }}</v-card-title>

      <v-card-text>
        <ul class="my-2">
          <li
            v-for="ingredient in recipe.IngredientsWithQuantity"
            :key="ingredient"
          >
            {{ ingredient }}
          </li>
        </ul>
        <h2 class="my-2">Preparation</h2>
        <ol>
          <li v-for="step in recipe.Steps" :key="step">{{ step }}</li>
        </ol>
      </v-card-text>

      <v-divider class="mx-4"></v-divider>

      <v-card-title>Perfect fit:</v-card-title>

      <v-card-text>
        <v-chip-group
        v-for="recom in recipe.Recommendations" :key="recom.Name"
          column
          v-model="selection"
          active-class="bell-colors accent-4 white--text"
        >
          <v-chip><a :href="recom.Url" target="_blank"> {{recom.Name}} </a></v-chip>
        </v-chip-group>
      </v-card-text>
    </v-card>
  </v-container>
</template>

<script>
import axios from "axios";

export default {
  name: "Recipe",

  data: () => ({
    selection: 0,
    recipes: [
      {
        Name: "Mami's Mini Chicken Burritos",
        Steps: [
          "In a large bowl, mix chicken with the beans, tomato, tomato sauce and jalapeno. Meanwhile, heat a large non-stick skillet over low heat. If you know how to heat tortillas in a griddle, then do so, otherwise warm them slightly in the microwave between paper towels. Place one tortilla in a plate. Put about 2 tbs filling in the center of the tortilla, top with a little cheese and lettuce, if desired. Fold the sides of the tortillas over the filling, then fold the side closest to you over the filling and turn burritos over, enclosing the filling. Repeat with remaining tortillas and filling. Place seam side down on the skillet so that the burritos get nice and crispy on the outside and the cheese melts. Serve hot.",
        ],
        IngredientsWithQuantity: [
          "1 1/2 cups cooked chicken breasts, shredded ",
          "1 cup cooked beans (pinto, black or whatever you like)",
          "1 roma tomato, chopped ",
          "1/4 cup tomato sauce",
          "1 -2 canned jalapeno chile, finely chopped ",
          "1/2 cup shredded cheese (Monterey. Manchego, Oaxaca, Cheddar)",
          "1 cup shredded lettuce",
          "10 regular size flour tortillas",
        ],
        Recommendations: [
          {
            Name: "POULET CERVECAS",
            Url: "https://www.bell.ch/de/produkte/alle-produkte/produkt/poulet-cervelas-116141/",
          },
        ],
      },
      {
        Name: "Southwestern Chicken Caesar Wrap",
        Steps: [
          "Heat chicken in pan, placing into a rectangular shape.",
          "As it heats, place 2 slices of cheddar on top until just melting.",
          "Meanwhile, layer lettuce and pico de gallo in center of wrap.",
          "Top lettuce and pico with heated chicken &amp; cheese pile.",
          "Top with caesar dressing.",
          "Wrap &amp; enjoy!",
        ],
        IngredientsWithQuantity: [
          "1 (12 inch) sun dried tomato tortillas",
          "4 ounces grilled chicken breast strips",
          "2 slices cheddar cheese",
          "2 tablespoons pico de gallo",
          "1 cup shredded lettuce",
          "2 tablespoons caesar salad dressing",
        ],
        Recommendations: [],
      },
      {
        Name: "Chicken Ranch Tacos - Quick and Easy",
        Steps: [
          "Heat oven to 325 degrees. Heat taco shells for 5-10 minutes to warm.",
          "Slice chicken into bite-able pieces and put into bowl.",
          "Warm up chicken in frying pan coated with olive oil adding chopped green onions.",
          "Stir in 1/2 cup of dressing to coat chicken.",
          "Spoon chicken mixture into taco shells.",
          "Top with lettuce, tomato, cheese and olives.",
          "Add the remaining dressing to each taco, if desired.",
        ],
        IngredientsWithQuantity: [
          "1 (5 ounce) box taco shells",
          "2 lbs chicken, precooked ",
          "1 (8 ounce) bottle ranch dressing",
          "1/2 cup shredded lettuce",
          "1 cup shredded sharp cheddar cheese",
          "1 cup chopped tomato",
          "1/4 cup chopped green onion",
        ],
        Recommendations: [
          {
            Name: "POULET BRATWURST",
            Url: "https://www.bell.ch/de/produkte/alle-produkte/produkt/poulet-bratwurst-116143/",
          },
        ],
      },
      {
        Name: "Chicken Sliders With Brie",
        Steps: [
          "In a bowl, combine mayo, pesto, and mustard. Mix well. Add chicken and toss to combine. Season to taste with salt and pepper.",
          "Spread cut side of buns with equal portions of chicken mixture. Lay cheese across top halves.",
          "Place tops on baking sheet and broil until cheese is just beginning to melt. Garnish with lettuce and tomatoes. Put tops and bottoms together and serve.",
        ],
        IngredientsWithQuantity: [
          "2 tablespoons mayonnaise",
          "2 tablespoons sun-dried tomato pesto",
          "1 teaspoon Dijon mustard",
          "2 cups cooked chicken, diced ",
          "16 slider buns",
          "8 ounces brie cheese, sliced ",
          " baby lettuce leaf",
          " sliced tomatoes",
        ],
        Recommendations: [],
      },
      {
        Name: "Naan Chicken Sandwich",
        Steps: [
          "Preheat oven to 450*F. Place bread on baking sheet and cook 5 minutes.",
          "Stir together chutney and tomato paste in a small bowl. Spread mixture over one piece of bread.",
          "Toss together remaing ingedents in a medium bowl and pile over tomato mixture. Top with remaning bread and cut into four pieces.",
        ],
        IngredientsWithQuantity: [
          " naan bread, 2 large (or 4 small)",
          "3 tablespoons mango chutney",
          "3 tablespoons tomato paste",
          "1 cup rotisserie-cooked chicken, diced (not cold)",
          "1 cup romaine lettuce, diced ",
          "2 tablespoons olive oil",
        ],
        Recommendations: [],
      },
      {
        Name: "Solo Greek Style Chicken Salad",
        Steps: [
          "Put the olive oil, vinegar, feta cheese, and black pepper into an individual serving salad bowl.",
          "Mix.",
          "Add the lettuce, tomatoes, and toss.",
          "Add the chicken and olives followed by the lemon juice.",
        ],
        IngredientsWithQuantity: [
          "1 cup diced cooked chicken",
          "1 cup romaine lettuce",
          "1 roma tomato, diced ",
          "1/2 cup black olives, pitted and diced ",
          "2 tablespoons olive oil",
          "1 tablespoon vinegar",
          "2 tablespoons feta cheese, crumbled ",
          "1 teaspoon black pepper",
          "1 lemon, just the juice ",
        ],
        Recommendations: [],
      },
      {
        Name: "Mexican Pizza",
        Steps: ['t walk away.", '],
        IngredientsWithQuantity: [
          "64 inches flour tortillas",
          " cooking spray",
          "1 cup salsa",
          "9 -10 ounces cooked chicken breasts",
          "2 cups shredded cheddar cheese",
          "1 cup shredded lettuce",
          "1/2 cup chopped fresh tomato",
          "1/2 cup guacamole or 1/2 cup chopped avocado",
        ],
        Recommendations: [],
      },
      {
        Name: "Easy Cheesy Chicken Tacos",
        Steps: [
          "In large skillet, cook chicken or turkey over medium-high heat until it is no longer pink.",
          "Stir in taco sauce.",
          "Continue cooking until mixture just starts to boil.",
          "Add cheese.",
          "Remove from heat and stir just until cheese is melted.",
          "Meanwhile, warm taco shells according to package directions.",
          "Spoon into taco shells and serve with lettuce, tomato, sour cream, black olives or any other taco fixings your family enjoys.",
        ],
        IngredientsWithQuantity: [
          "1 lb ground chicken (I use ground turkey)",
          "1 cup taco sauce (you choose the heat)",
          "2 cups shredded Mexican blend cheese",
          "10 taco shells",
          " shredded lettuce",
          " sour cream",
          " sliced black olives",
          " chopped tomato",
        ],
        Recommendations: [],
      },
      {
        Name: "Calamity Jane Salad",
        Steps: [
          "Heat the tortilla on both sides on a hot dry griddle until bubbles appear.",
          "Heating the chicken breast is optional, but a microwave works well if you choose to do so.",
          "Place the tortilla on a plate and top with chicken, lettuce, tomato, avocado, and onion.  Spoon the sour cream and salsa in dollops all over the other ingredients.  Serve open face or fold the tortilla in half over the salad.",
          "You may garnish with chopped jalapeno, black olives, crisp bacon bits, or cilantro if you like.  Be aware,  however, that olives, bacon, or cilantro will alter the taste.",
        ],
        IngredientsWithQuantity: [
          "1 (12 inch) whole wheat tortillas",
          "2 leaves romaine lettuce, sliced in thin strips ",
          "1 cooked chicken breast, boned, skinned, chopped ",
          "1 roma tomato, chopped ",
          "1 slice sweet onion, chopped fine ",
          "1/2 avocado, chopped ",
          "1/4 cup sour cream",
          "1/4 cup salsa",
        ],
        Recommendations: [
          {
            Name: "POULET WIENERLI",
            Url: "https://www.bell.ch/de/produkte/alle-produkte/produkt/poulet-wienerli-116142/",
          },
        ],
      },
      {
        Name: "Daily Burn Cobb Salad",
        Steps: [
          "Toss chopped heart of romaine in Dijon dressing.",
          "Arrange chopped chicken, tomatoes, avocado, and egg on top of spinach and dig in!",
        ],
        IngredientsWithQuantity: [
          "1 whole egg",
          "2 cups of chopped romaine lettuce",
          "1 whole chicken breast",
          "1/2 whole avocado",
          "1/2 whole tomato",
          "1 tablespoon Dijon mustard",
          "1 tablespoon olive oil",
          "1/2 teaspoon apple cider vinegar",
        ],
        Recommendations: [],
      },
    ],
  }),
  created() {
    const base_url = "http://ec2-18-156-118-115.eu-central-1.compute.amazonaws.com";
    const body = JSON.stringify({});
    axios
      .post(base_url + "/create-recipie/", body)
      .then((response) => console.log(response.data))
      .catch((error) => {
        this.errorMessage = error.message;
        console.error("There was an error!", error);
      });
  },
};
</script>

<style scoped>
.bell-colors {
  background-color: #d9161b;
  color: white !important;
}
 /* unvisited link */
 a:link {
  color: white;
}

/* visited link */
a:visited {
  color: white;
}

/* mouse over link */
a:hover {
  color: white;
}

/* selected link */
a:active {
  color: white;
} 
</style>