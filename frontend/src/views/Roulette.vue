<script lang="ts" setup>
import { computed, ref } from 'vue';
import { useRouter } from 'vue-router'
import { useWalletStore } from '../stores/wallet'
import { API_URL } from '../constants'
// 
const router = useRouter();
const wallet = useWalletStore();

const username = wallet.username;
if (username == '') {
    router.push('/')
}

const balance = computed(() => wallet.currentBalance);

const amount = ref();

const betNumber = ref();
const betNumberEven = ref();
const betColor = ref();
const betAmount = ref();

const showBet = ref(false);
const betResult = ref({
    WinColor: false,
    WinNumberEven: false,
    WinNumber: false,
    Win: false,
    Amount: 0,
    Game: {
        Color: 0,
        Number: 0,
        IsEvenNumber: false
    }
});

const CreditBalance = async () => {
    if(amount.value <= 0) {
        alert("Ingrese un valor mayor a cero");
        return;
    }

    const body =  JSON.stringify({ username, amount: amount.value })
    var response = await fetch(`${API_URL}/balance`, { 
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        method: 'POST', 
        body 
    })
    var result = await response.json();
    wallet.setWallet(result.username, result.amount);
    amount.value = 0;
    alert(`Su saldo actual es ${result.amount}`);
}

const Game = async () => {

    if(betAmount.value == undefined || betAmount.value <= 0) {
        alert("ingrese un valor mayor a cero");
        return;
    }

    if(betAmount.value > balance.value) {
        alert("El monto ingresado es mayor al saldo que tiene. Por favor, ingrese saldo o apueste por menor valor.")
        return;
    }

    const body = JSON.stringify({ 
        username, 
        betAmount: betAmount.value, 
        number: betNumber.value, 
        numberEven: betNumberEven.value,
        color: betColor.value
    });

    var response = await fetch(`${API_URL}/Roulette`,
        {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            method: 'POST', 
            body
        }
    );
    var result = await response.json();
    wallet.setWallet(username, result.Balance.amount);
    betResult.value = result;
    showBet.value = true;
}

</script>

<template>
    <div class="col-4">
        <h1>Juego de la Ruleta</h1>
        <small>Bienvenido <b>{{ username }}</b> - Su balance es <b>${{ balance }}</b></small>
        <hr>
        <div class="mb-3">
            <div class="row g-3 align-items-center">
                <div class="col-auto">
                    <label for="amount" class="form-label">Ingresar Salgo</label>
                </div>
                <div class="col-auto">
                    <input 
                        v-model="amount" 
                        @keyup.enter="CreditBalance"
                        type="number" 
                        name="amount" 
                        id="amount" 
                        min="1" 
                        max="9999" 
                        class="form-control" 
                        style="width: 100px;">
                </div>
                <div class="col-auto">
                    <button 
                        type="button" 
                        class="btn btn-sm btn-success" 
                        @click="CreditBalance" >
                        Ingresar
                    </button>
                </div>
            </div>
        </div>
      <hr>
      <h3>Apuesta</h3>

      <div class="row mt-2">
        <div class="col-auto">
            <label for="betAmount">Ingrese el monto de la apuesta</label>
        </div>
        <div class="col-auto">
            <input v-model="betAmount" name="betAmount" id="betAmount" type="number" class="form-control">
        </div>
      </div>
      <div class="row mt-2">
        <div class="col-auto">
            <label for="betNumber">Puede apostar por un Número</label>
        </div>
        <div class="col-auto">
            <input v-model="betNumber" name="betNumber" id="betNumber" type="number" class="form-control">
        </div>
      </div>
      <div class="row mt-2">
        <div class="mb-1">
            <label for="betNumberEven">Puede apostar por si el Número es Par/Impar</label>
        </div>
        <div class="mb-1">
            <div class="form-check">
                <input v-model="betNumberEven" value="true" class="form-check-input" type="radio" name="betNumberEven" id="betNumberEven1">
                <label class="form-check-label" for="betNumberEven1">
                    Par
                </label>
            </div>
            <div class="form-check">
                <input v-model="betNumberEven" value="false" class="form-check-input" type="radio" name="betNumberEven" id="betNumberEven2">
                <label class="form-check-label" for="betNumberEven2">
                    Impar
                </label>
            </div>
        </div>
      </div>
      <div class="row">
        <div class="mb-1">
            <label for="betColor">Puede apostar por el Color</label>
        </div>
        <div class="mb-1">
            <div class="form-check">
                <input v-model="betColor" value="0" class="form-check-input" type="radio" name="betColor" id="betColor1">
                <label class="form-check-label" for="betColor1">
                    Rojo
                </label>
            </div>
            <div class="form-check">
                <input v-model="betColor" value="1" class="form-check-input" type="radio" name="betColor" id="betColor2">
                <label class="form-check-label" for="betColor2">
                    Negro
                </label>
            </div>
        </div>
      </div>
      <br>
      <button 
            type="button" 
            class="btn btn-sm btn-success" 
            @click="Game" >
            Apostar
      </button>
      <hr>
      <div class="alert" :class=" {'alert-success': betResult.Win, 'alert-danger': !betResult.Win}  " role="alert" v-show="showBet">
        Resultado de la apuesta: <br>
        Color: <b>{{ betResult.Game.Color == 0 ? 'Rojo' : 'Negro' }}</b> -
        Número: <b>{{ betResult.Game.Number }}</b> -
        Número Par: <b>{{ betResult.Game.IsEvenNumber ? 'Si' : 'No' }}</b>
        <hr>
        Gano por Color: <b>{{ betResult.WinColor ? 'Si' : 'No' }}</b> <br>
        Gano por Número Par/Impar: <b>{{ betResult.WinNumberEven ? 'Si' : 'No' }}</b> <br>
        Gano por Número y Color: <b>{{ betResult.WinNumber ? 'Si' : 'No' }}</b> <br>
        <hr>
        <div v-if="betResult.Win">
            Gano <b>${{betResult.Amount}} 😂🕺🏽</b>
        </div>
        <div v-else>
            Perdio <b>${{betResult.Amount}} 😢</b>
        </div>
        Su saldo actual es <b>${{ balance }}</b>
      </div>
    </div>
</template>