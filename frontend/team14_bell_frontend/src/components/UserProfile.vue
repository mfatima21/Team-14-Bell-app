<template>
  <v-col align="center" justify="center">
    <!-- AVATAR START -->
    <v-row align="center" justify="center">
      <v-avatar class="mt-1" size="56">
        <img src="../assets/avatar.jpeg"/>
      </v-avatar>
    </v-row>
    <!-- AVATAR END -->

    <v-row align="center" justify="center">
      <v-card width="90%" class="mt-2" elevation="2">
        <v-row align="center" justify="center">
          <v-card-title>Hello Max!</v-card-title>
        </v-row>
        <v-row align="center" justify="center">
          <!-- PREFERENCES CHECKBOX LIST START -->
          <v-col cols="6" xs="6">
            <v-icon class="green-icon">mdi-noodles</v-icon>
            <v-checkbox
              v-for="preference in preferences"
              :key="preference.id"
              :input-value="preference.ticked"
              :value="preference.ticked"
              disabled
              :label="preference.name"
              dense
            ></v-checkbox>
          </v-col>
          <!-- PREFERENCES CHECKBOX LIST END -->

          <!-- ALLERGIES CHECKBOX LIST START -->
          <v-col cols="6" xs="6">
            <v-icon class="orange-icon">mdi-food-off-outline</v-icon>
            <v-checkbox
              v-for="allergy in allergies"
              :key="allergy.id"
              :input-value="allergy.ticked"
              :value="allergy.ticked"
              disabled
              :label="allergy.name"
              dense
            ></v-checkbox>
          </v-col>
          <!-- ALLERGIES CHECKBOX LIST END -->
        </v-row>

        <!-- BUTTONS START -->
        <v-card-actions>
          <v-btn class="blue" elevation="2" icon>
            <v-icon class="white-icon">mdi-pencil</v-icon> <!-- TODO: EDIT -->
          </v-btn>
          <v-btn class="blue" elevation="2" icon>
            <v-icon class="white-icon">mdi-lock-reset</v-icon> <!-- TODO: RESET -->
          </v-btn>
          <v-spacer></v-spacer>
          <v-btn class="blue" elevation="2" icon>
            <v-icon class="white-icon">mdi-format-list-checkbox</v-icon> <!-- TODO: SHOPPING LIST -->
          </v-btn>
        </v-card-actions>
        <!-- BUTTONS END -->
      </v-card>
    </v-row>
  </v-col>
</template>

<script>
export default {
  name: "UserProfile",

  data: () => ({
    preferences: [
      { id: 1, name: "Vegan", ticked: false },
      { id: 2, name: "Vegetarian", ticked: true },
      { id: 3, name: "Halal", ticked: false },
      { id: 4, name: "Kosher", ticked: false },
    ],
    allergies: [
      { id: 1, name: "Lactose", ticked: true },
      { id: 2, name: "Gluten", ticked: false },
      { id: 3, name: "Mustard", ticked: false },
      { id: 4, name: "Sellery", ticked: false },
    ],
  }),
};
</script>

<style scoped>
.white-icon {
  color: white !important;
}
.green-icon {
  color: green !important;
}
.orange-icon {
  color: orange !important;
}
</style>
