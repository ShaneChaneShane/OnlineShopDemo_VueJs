<template>
  
  <div class="min-h-screen bg-gray-200 pb-10 pt-0">
       <!-- Banner Section -->
    <div class="banner relative">
      <img src="@/assets/banner.jpg" alt="Banner Image" class="top-0 w-full h-48 object-cover" />
      <div class="absolute top-0 left-0 w-full h-full bg-black bg-opacity-50 flex items-center justify-center text-center">
        <div>
          <h1 class="text-4xl text-white font-bold">Welcome to Our Shop</h1>
          <p class="text-xl text-white mt-2">Find the best deals and exclusive items</p>
        </div>
      </div>
    </div>


    <!-- Navigation Menu -->
    <nav class="bg-gray-800 text-white py-4">
      <ul class="flex justify-center space-x-8">
        <li v-for="menu in menus" :key="menu.name" class="relative">
          <button
            @click="toggleMenu(menu.name)"
            class="font-semibold px-4 py-2 hover:bg-gray-700 rounded focus:outline-none"
          >
            {{ menu.name }}
          </button>
          <transition name="fade-slide">
            <ul
              v-if="currentMenu === menu.name"
              class="absolute left-0 top-full bg-gray-700 text-white mt-2 rounded shadow-lg"
            >
              <!-- Conditional rendering of submenu items, skipping "Home" -->
              <template v-if="menu.name !== 'Home'">
                <li
                  v-for="(submenuItem, i) in menu.submenus"
                  :key="'submenu-item-' + i"
                  class="px-4 py-2 hover:bg-gray-600 cursor-pointer"
                >
                  {{ submenuItem }}
                </li>
              </template>
            </ul>
          </transition>
        </li>
      </ul>
    </nav>


    <!-- Content Section -->
    <div class="py-10 px-4">
      <header class="text-center mb-12">
        <h2 class="text-4xl font-bold text-gray-800">Online Shop</h2>
        <p class="text-xl text-gray-600">Explore our exclusive collection of items</p>
      </header>
    </div>


    <!-- Add to Cart Success Message -->
    <div v-if="showSuccessMessage" class="success-message fixed top-0 bg-green-600 text-white py-2 px-4 rounded-lg shadow-lg" :class="{ hide: !showSuccessMessage }">
      Added to cart!
    </div>


    <!-- Filter and Sort Section -->
    <div class="flex justify-between max-w-6xl mx-auto px-4 mb-6">
      <div>
        <label for="category" class="text-lg text-gray-700">Filter by Category:</label>
        <select id="category" v-model="selectedCategory" class="p-2 rounded border border-gray-300">
          <option value="">All</option>
          <option value="electronics">Electronics</option>
          <option value="clothing">Clothing</option>
          <option value="home">Home</option>
        </select>
      </div>
      <div>
        <label for="sort" class="text-lg text-gray-700">Sort by:</label>
        <select id="sort" v-model="selectedSort" class="p-2 rounded border border-gray-300">
          <option value="price_asc">Price: Low to High</option>
          <option value="price_desc">Price: High to Low</option>
          <option value="name_asc">Name: A to Z</option>
          <option value="name_desc">Name: Z to A</option>
        </select>
      </div>
    </div>

    <!-- Product List Section -->

    <div class="max-w-6xl mx-auto px-4 grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-8">
      <ProductItem 
        v-for="product in filteredAndSortedProducts" 
        :key="product.id" 
        :product="product" 
        @add-to-cart="addToCart"
      />
    </div>

    <!-- Shopping Cart Section -->
    <div class="fixed bottom-0 right-0 m-6 bg-emerald-500 text-white p-4 rounded-lg">
      <button @click="toggleCart" class="text-xl">Shopping Cart ({{ cart.length }})</button>
      <div v-if="isCartVisible" class="cart bg-white p-4 mt-2 rounded-lg shadow-lg" :class="{ hidden: !isCartVisible }">
        <h3 class="font-semibold text- text-black">Your Cart</h3>
        <ul>
          <li v-for="item in cart" :key="item.id" class="flex justify-between py-2 border-b text-black">
            <span  class="mr-4">{{ item.name }} - ${{ item.price }}</span>
            <button @click="removeFromCart(item.id)" class="text-red-600">Remove</button>
          </li>
        </ul>
        <div v-if="cart.length > 0" class="mt-4 text-right">
          <button @click="buyItems" class="bg-green-600 text-white p-2 rounded-lg">
            Buy Now - ${{ totalCartPrice }}
          </button>
        </div>
      </div>
    </div>


    <div class="text-center pt-4 px-4">
        <p class="text-sm text-gray-600 mt-2">
          This website is a personal project developed to explore and enhance web development skills. 
          Designed and built by ShaneChaneShane on Github</p>
    </div>
  </div>
</template>

<script>
import { fetchProducts } from '@/services/api';
import ProductItem from "@/components/ProductItem.vue";
import './CartAnimation.css';
export default {
  components: {
    ProductItem,
  },
  data() {
    return {
      products: [

      ],
      selectedCategory: "",
      selectedSort: "price_asc",
      cart: [],
      isCartVisible: false,
      showSuccessMessage: false, 
   
      menus: [
        { name: "Home", submenus: ["Submenu 1", "Submenu 2", "Submenu 3"] },
        { name: "Shop", submenus: ["Electronics", "Clothing", "Home"] },
        { name: "About", submenus: ["Our Story", "Team", "Careers"] },
        { name: "Contact", submenus: ["Email", "Phone", "Support"] },
      ],
      currentMenu: null, // Tracks the currently active menu
    };
  },
  async created() {
        try {
          const response = await fetchProducts();
          this.products = response;
        } catch (error) {
            console.error('Failed to fetch products:', error);
        }
  },
  computed: {
    filteredAndSortedProducts() {
      let products = this.products;

      // Filter by Category
      if (this.selectedCategory) {
        products = products.filter((product) => product.category === this.selectedCategory);
      }

      // Sort by Price or Name
      if (this.selectedSort === "price_asc") {
        products.sort((a, b) => a.price - b.price);
      } else if (this.selectedSort === "price_desc") {
        products.sort((a, b) => b.price - a.price);
      } else if (this.selectedSort === "name_asc") {
        products.sort((a, b) => a.name.localeCompare(b.name));
      } else if (this.selectedSort === "name_desc") {
        products.sort((a, b) => b.name.localeCompare(a.name));
      }

      return products;
    },
    totalCartPrice() {
      return this.cart.reduce((total, item) => total + item.price, 0);
    },
  },
  methods: {
    addToCart(product) {
      this.cart.push(product);
      this.showSuccessMessage = true;  // Show the success message
      setTimeout(() => {
        this.showSuccessMessage = false;  // Hide the success message after 2 seconds
      }, 2000);
    },
    removeFromCart(productId) {
      this.cart = this.cart.filter((item) => item.id !== productId);
    },
    toggleCart() {
      this.isCartVisible = !this.isCartVisible;
    },
    buyItems() {
      alert(`Purchased ${this.cart.length} items for $${this.totalCartPrice}`);
      this.cart = [];
    },
    toggleMenu(menuName) {
      this.currentMenu = this.currentMenu === menuName ? null : menuName;
    },
  },
};
</script>

<style scoped>
.fade-slide-enter-active,
.fade-slide-leave-active {
  transition: all 0.3s ease;
}
.fade-slide-enter {
  opacity: 0;
  transform: translateY(-10px);
}
.fade-slide-leave-to {
  opacity: 0;
  transform: translateY(-10px);
}

.product-list {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
  gap: 1.5rem;
}
</style>
