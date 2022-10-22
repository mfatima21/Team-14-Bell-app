<template>
  <v-container>
    <v-row class="mt-2" align="center" justify="center">
        <v-img src="../assets/bell.jpeg" max-width="100" max-height="auto">
        </v-img>
        <span>Belliscious!</span>
      </v-row>
    <v-row class="text-center">
      <v-col cols="12">
        <v-file-input
          truncate-length="15"
          accept="image/png, image/jpeg, image/bmp"
          prepend-icon="mdi-camera"
          label="Upload Image"
        ></v-file-input>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
export default {
  name: "HelloWorld",

  data: () => ({}),
};
</script>
