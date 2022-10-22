<template>
  <v-app>
    <v-app-bar app color="primary" dark>
      <v-btn v-if="isMain" href="/" text>
        <span class="mr-2"></span>
        <v-icon>mdi-arrow-left</v-icon>
      </v-btn>

      <v-spacer></v-spacer>

      <v-btn href="/user" text>
        <span class="mr-2"></span>
        <v-icon>mdi-account</v-icon>
      </v-btn>
    </v-app-bar>

    <v-main>
      <router-view />
    </v-main>
  </v-app>
</template>

<script>
export default {
  name: "App",

  data: () => ({
  }),
  computed: {
    isMain() {
      return this.$route.name !== "home";
    },
  },
};
</script>
