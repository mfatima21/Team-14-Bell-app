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

      <v-divider v-if="recipe.Recommendations.length > 0" class="mx-4"></v-divider>

      <v-card-title v-if="recipe.Recommendations.length > 0" >Perfect fit:</v-card-title>

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
    recipes: []
  }),
  created() {
    const base_url = "http://ec2-18-156-118-115.eu-central-1.compute.amazonaws.com";
    // const base_url = "http://localhost:3000";

    let data = this.$route.params.data;

    axios
      .post(base_url + "/create-recipie/", data.ingredients)
      .then((response) => this.recipes = response.data)
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