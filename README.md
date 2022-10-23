# Team-14-Bell-app
Basel Hack 2022 Bell Challenge

## Problem Statements
Bell proposes to solve a few problems. The major one is fighting foodwaste by enabling people to get recipe suggestions supplying an image of their fridge content. The second problem is to get to know Bell customers better to provide a tailored Bell experience to them.
The solutions on the market are scattered APIs or unfinished solutions. They dont include the companies' need to have their products suggested to their customers.

## Solutions
We have created a proof of concept: a webapp, developed in a mobile first concept, which merges open APIs, enriches the data with Bell products, and suggests recipes to customers.
We are using an API to which an image can be sent, and the products are recognized. Then the products are enhanced with Bell products and send to a second API which sends back recipe suggestions. These are displayed to the user. 

## Technical Prerequisites
- Node
- NPM

## Uses
- VueJS 2
- Vuetify
- VueRouter
- Axios

## Backend Setup
```
git pull
npm install
node server.js
```

## APIs for Local Use
localhost:3000/create-recipie
localhost:3000/recognize-image/

## Frontend Setup
```
npm install
```

### Compiles and hot-reloads for development
```
npm run serve
```

### Compiles and minifies for production
```
npm run build
```

### Lints and fixes files
```
npm run lint
```

:meat_on_bone: :computer: :green_heart: :poultry_leg: :rainbow:

### Documentation

The google presentation is [here](https://docs.google.com/presentation/d/15kZCKr3lPAo_j7qtFoXbP1KHa1h_AUIR23ijlMK8sqs/edit?usp=sharing)

### URL to Check out the PoC
http://ec2-18-156-118-115.eu-central-1.compute.amazonaws.com