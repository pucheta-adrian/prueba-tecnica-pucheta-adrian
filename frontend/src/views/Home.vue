<script setup lang="ts">
    import { ref } from 'vue';
    import { useRouter } from 'vue-router';
    import { useWalletStore } from '../stores/wallet'
    import { API_URL } from '../constants'

    const router = useRouter();
    const loading = ref(false);
    const username = ref("");
    const wallet = useWalletStore();

    
    console.log(import.meta.env);
    const next = async () => {
        loading.value = true;
        var response = await fetch(`${API_URL}/balance/${username.value}`);
        var result = await response.json()
        //
        wallet.setWallet(result.username, result.amount);
        loading.value = true;

        router.push('/game');
    }
</script>

<template>
    
    <div class="col-4">
      <div class="mb-3 mt-5">
        <label for="username" class="form-label">Nombre de usuario</label>
        <input type="text" name="username" id="username" v-model="username" class="form-control" autofocus>
      </div>
      <button 
        type="button" 
        class="btn btn-sm btn-primary" 
        @click="next" 
        @keyup.enter="next">
        Ingresar
      </button>
    </div>
    
</template>
