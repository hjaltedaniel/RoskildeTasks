# client

## Project setup
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

### Run your unit tests
```
npm run test:unit
```

### Lints and fixes files
```
npm run lint
```

### Customize configuration
See [Configuration Reference](https://cli.vuejs.org/config/).

## Details about project configuration

### Vue component generator
```npm install -g vue-generate-component```

This is a global package you have to install in order to generate components. 

See [vue-generate-component package reference](https://www.npmjs.com/package/vue-generate-component)


### Vue configuration
The ```vue.config.js``` file can be found in the root folder of the client and it specifies that all sass/scss files should be prepended with ```@import "~@/global-styles/variables.scss";``` This way you do not need to import styling variables anymore.

### Unit Testing
The project uses Jest for unit testing.
All tests are placed under ```client/tests/unit/**/*.spec.js```

See [Jest reference](https://jestjs.io/)

### Bootstrap
Bootstrap has been chosen in favor of other CSS frameworks (such as Bulma) because it is very well documented, highly reliable and includes a large about of features and customizability. 

See [Bootstrap reference](https://getbootstrap.com/)

## src Folder Structure
Under ```client/src/``` there are a few main folders:
- ```assets``` any static assets will go here (e.g images and fonts);
- ```views``` here you can find the main "views" of the application, you can think of this folder as "Pages"
- ```components``` this folders contains the components that make up the views
- ```global-styles``` any SASS files that apply gloabally
- ```service``` Any modules that are responsible for mediating communication between the client and the application server
- ```router``` this folder holds anything that is realted to routing (e.g. main routing file, guards)