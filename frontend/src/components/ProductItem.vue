<template>
    <div class="bg-white shadow-lg rounded-lg p-6 flex flex-col items-center">
      <img :src="product.image" alt="Product Image" class="w-full h-48 object-cover rounded-lg mb-4" />
      <h2 class="text-xl font-semibold text-gray-700">{{ product.name }}</h2>
      <p class="text-gray-500 mt-2">${{ product.price }}</p>
      <button 
        @click="$emit('add-to-cart', product)" 
        class="mt-4 px-6 py-2 bg-green-950 text-white rounded-lg hover:bg-green-800 focus:outline-none"
      >
        Add to Cart
      </button>
    </div>
  </template>
  
  <script>
  export default {
    props: {
      product: {
            type: Object,
            required: true,
        },
    },
    methods: {
      addToCart() {
        this.$emit("add-to-cart", this.product);
      },
    },
  };
  </script>
  
  <style scoped>
  
  </style>
  