import { createRouter, createWebHistory } from 'vue-router'
import Home from '../views/Home.vue';
import Roulette from '../views/Roulette.vue';

const routes = [
    { path: '/', component: Home },
    { path: '/game', component: Roulette }
]

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;