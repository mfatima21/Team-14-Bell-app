<template>
  <v-container>
    <v-row class="mt-2" align="center" justify="center">
        <v-img src="../assets/bell.jpeg" max-width="100" max-height="auto">
        </v-img>
        <span>Bellicious!</span>
      </v-row>
    <v-row class="text-center">
      <v-col cols="12">
        <v-file-input
          truncate-length="15"
          accept="image/png, image/jpeg, image/bmp"
          prepend-icon="mdi-camera"
          @change="onFileChange"
          label="Upload Image"
        ></v-file-input>
        <v-btn
      depressed
      color="primary"
      href="/recipe"
    >
      Send
    </v-btn>
      </v-col>
    </v-row>
    <v-row>
      <li v-for="item in ingredients" :key="item">{{item}}</li>
    </v-row>
  </v-container>
</template>

<script>
export default {
  name: "HelloWorld",

  data: () => ({
    ingredients: []
  }),

  methods: {
    onFileChange: function(e) {
      var self = this;

      var formdata = new FormData();
      formdata.append("fridge_image", e, "image.jpeg");

      var requestOptions = {
        method: 'POST',
        body: formdata,
        redirect: 'follow'
      };

      // const base_url = "http://ec2-18-156-118-115.eu-central-1.compute.amazonaws.com";
      const base_url = "http://localhost:3000";
      const endpiont = base_url + "/recognize-image/"

      fetch(endpiont, requestOptions)
          .then(response => response.text())
          .then(result => {
            const obj = JSON.parse(result);
            const uniquefoodItems = [...new Set(obj.foodItems)];
            self.ingredients = uniquefoodItems;
          })
          .catch(error => console.log('error', error));
    }
  }

};
</script>
