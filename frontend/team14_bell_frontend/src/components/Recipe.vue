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
          column
          v-model="selection"
          active-class="success accent-4 white--text"
        >
          <v-chip>Bell Merguez</v-chip>
          <v-chip>Bell Cervelas</v-chip>
          <v-chip>Bell Angus</v-chip>
        </v-chip-group>
      </v-card-text>
    </v-card>
  </v-container>
</template>

<script>
import axios from 'axios'

export default {
  name: "Recipe",

  data: () => ({
    selection: 1,
    recipes: [
      {
        Name: "Rinktum Ditty",
        Steps: [
          "Put the stick of butter in a skillet and brown the onions.",
          "Then add the tomato soup.",
          "Now cut the cheese into chunks and simmer until the cheese melts.",
          "Put and serve over crackers.",
        ],
        IngredientsWithQuantity: [
          "1 (10 1/4 ounce) can tomato soup (1-2 cans)",
          "1/2 cup butter",
          "1 pinch onion",
          "1 (8 ounce) package sharp cheddar cheese",
        ],
      },
      {
        Name: "20 MINUTE BUTTER AND ONION TOMATO SAUCE",
        Steps: [
          "Heat the ingredients in a small or medium covered saucepan, over high heat. When the sauce starts to boil, slightly reduce heat to medium-high and leave the lid ajar. Continue cooking for another 15-20 minutes.",
          "In the meantime, bring a large pot of salted water to a boil and cook the pasta according to package instructions. Discard the onion from the sauce and toss with well drained pasta. Sprinkle a couple of handfuls of your choice of grated Italian cheese and serve.",
        ],
        IngredientsWithQuantity: [
          "2 cups plain pureed tomato sauce",
          "5 tablespoons unsalted butter",
          "1 medium onion, peeled and cut in half ",
          "1 -2 handfuls italian grated cheese (Parmesan, Grana Padano, or Romano)",
          " bavette pasta (or other long pasta)",
        ],
      },
      {
        Name: "Fancy Pants",
        Steps: [
          "Shape hamburger into 8 patties.",
          "Fold slices of cheese into 4 small squares; put each square between 2 hamburger patties.",
          "t melt through.\u0022, ",
        ],
        IngredientsWithQuantity: [
          "2 lbs hamburger, seasoned to taste ",
          "1 (8 ounce) can tomato sauce",
          "4 slices American cheese",
          "1 small onion, diced ",
        ],
      },
      {
        Name: "Crockpot Baked Ziti",
        Steps: [
          "Combine cheeses in large bowl, reserving some mozzarella. Brown meat with onion, garlic and oregano or Italian seasoning. Drain and return to pan. Add tomato sauce and tomato paste. Stir well and heat through. Spray interior of slow cooker. Place a small layer of meat and sauce mixture on bottom. Cover with dry pasta. Add several spoons of cheese mixture, spread over pasta. Add a layer of sauce, a layer of pasta, a layer of cheese. Repeat until all ingredients are gone. Cook on low 6 hours. Ten minutes before serving, sprinkle reserved mozzarella on top and allow cheese to melt.",
        ],
        IngredientsWithQuantity: [
          "1 lb ground beef or 1 lb turkey",
          "1 medium onion, chopped ",
          "2 teaspoons minced garlic",
          " oregano or Italian herb seasoning, to taste ",
          "1 (29 ounce) can tomato sauce",
          "1 (6 ounce) can tomato paste",
          "1 lb pasta",
          "1 (16 ounce) container cottage cheese",
          "1/2 cup grated parmesan cheese",
          "1 (16 ounce) bag shredded mozzarella cheese",
        ],
      },
      {
        Name: "Baked Spaghetti Paula Deen - Fast Version",
        Steps: [
          "Crumble Ground Beef into a large skillet.",
          "Cook over medium-high until no pink is in meat.",
          "Drain Fat from meat.",
          "Saute onions in small pan until soft.",
          "Add meat to 2 cups of bottled tomato sauce",
          "Add 1 cup of diced tomatoes, onion and pimetos",
          "Simmer for 20 minutes or more.",
          "Cook spaghetti as per package.",
          "Cover bottom of 13 x 9  x 2 pan with sauce.",
          "Add a layer of pasta and less than 1/2 of each of the cheeses.",
          "repeat layers ending with sauce on top.",
          "Bake in oven for 30 minutes.",
          "Top Casserole with remaining cheese.",
          "Cook about 5 more minutes.",
        ],
        IngredientsWithQuantity: [
          "1 cup canned diced tomato",
          "1 (16 ounce) jar tomato sauce",
          "1 1/2 lbs ground beef",
          "8 ounces spaghetti",
          "1 cup grated cheddar cheese",
          "1 cup grated monterey jack cheese",
          "1 onion",
          "1 (6 ounce) jar pimientos",
        ],
      },
      {
        Name: "Tomato Cheese Fondue",
        Steps: [
          "Simmer the tomato juice, lemon and soup mix for about 10 minutes, stirring often.",
          "Add the shredded cheese one handful at a time, melting between each handful.",
          "When it is all melted you are done.",
          "We always use bread but you could use veggies or meat.",
          "It can be a bit salty so I think that is why bread works best.",
          "Also if you use a brand name onion soup mix I think its less likely to be too salty.",
        ],
        IngredientsWithQuantity: [
          "1 cup tomato juice",
          "1 tablespoon lemon juice",
          "1 (1 1/4 ounce) envelope onion soup mix",
          "4 cups shredded sharp cheddar cheese",
        ],
      },
      {
        Name: "Mexican Soupa",
        Steps: [
          "Fry noodles in butter until brown and crunchy.",
          "Fry hamburger, onion, corn and salt to taste.",
          "Add both to boiling water with tomato sauce and salsa in it.",
          "Boil until noodles are done to your liking.",
          "Add to bowls top with a whole bunch of cheddar cheese.",
        ],
        IngredientsWithQuantity: [
          "5 -6 cups noodles",
          "5 (15 ounce) cans tomato sauce (5-6 cans)",
          "1 -2 lb hamburger",
          "1 whole onion, diced ",
          "2 tablespoons butter",
          " salt",
          "4 -6 cups cheese",
          "1 (15 ounce) can corn",
          "1 -2 cup salsa",
        ],
      },
      {
        Name: "Rondele Potato Salad",
        Steps: [
          "Peel and dice potatoes, place in large pot.  Cover with water and boil until potatoes flake when touched by a fork.",
          "While potatoes are cooking, brown and crumble the bacon.",
          "Drain potatoes and pour them into a large bowl.",
          "Add bacon crumbs, sundried tomatoes, onion, and entire tub of Rondele cheese to potatoes.  Stir to blend well.",
          "Serve hot or cold.",
        ],
        IngredientsWithQuantity: [
          "1 (8 ounce) container rondele garlic \\u0026 herb cheese spread",
          "8 medium potatoes",
          "1/2 cup sun-dried tomato, diced ",
          "1/2 cup vidalia onion, diced ",
          "6 slices bacon",
        ],
      },
      {
        Name: "Spaghetti Bolognese",
        Steps: [
          "Brown onion and meat until well done.",
          "Meanwhile cook spaghetti.",
          "Add traditional sauce to meat  and  flavor with Italian seasonings.",
          "When spaghetti is done, drain and serve with sauce.",
        ],
        IngredientsWithQuantity: [
          "1 lb ground beef",
          "1 chopped onion",
          "1 (28 ounce) can tomato sauce",
          "1 tablespoon oregano",
          "1 lb spaghetti",
          " parmesan cheese",
        ],
      },
      {
        Name: "Bacon and Cheese Hot Dish",
        Steps: [
          "Cook macaroni.",
          "Cut bacon into small pieces and fry until almost done. Drain off grease, add onion and finish cooking.",
          "Mix salt, pepper and tomato sauce, then add cooked bacon and onion.",
          "Put cooked macaroni in a 13x9 inch pan then pour tomato mixture over macaroni, top with cheese.",
          "Bake at 350 for 30-40 minute.",
        ],
        IngredientsWithQuantity: [
          "2 cups macaroni",
          "1 lb sliced bacon",
          "1 medium chopped onion",
          "2 (15 ounce) cans tomato sauce",
          "1/4 teaspoon salt",
          "1/8 teaspoon pepper",
          "1/4 lb cubed Velveeta cheese",
        ],
      },
    ],
  }),
  created() {
    const base_url = "http:" + window.location.href.split(':')[1]
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
