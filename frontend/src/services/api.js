
console.log("API URL:", process.env.VUE_APP_API_URL);
import axios from "axios";
import { mockProducts } from "@/mockData";
const apiClient = axios.create({
    baseURL: process.env.VUE_APP_API_URL, // Backend API base URL
    headers: { "Content-Type": "application/json" }
});

export const fetchProducts  = async () => {
    if (process.env.VUE_APP_API_MODE === 'mock') {
        console.log('Loading mock products');
        return mockProducts;  // Return mock data when in mock mode
      } else {
        try {
          const response = await apiClient.get("/products");
          return response.data;
        } catch (error) {
          console.error('Error fetching products:', error);
          throw error;
        }
      }
    };

export const    addProduct = async (product) => {
    if (process.env.VUE_APP_API_MODE === 'mock') {
        console.log('Adding mock product');
        mockProducts.push(product);  // Add to mock data
        return product;  // Return the added product
      } else {
        try {
          const response = await apiClient.post("/products", product);
          return response.data;
        } catch (error) {
          console.error('Error adding product:', error);
          throw error;
        }
      }
    };
