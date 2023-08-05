import { defineStore } from 'pinia'

export const useWalletStore = defineStore('wallet', {
    state: () => ({
        username: '',
        balance: 0.0
    }),
    actions: {
        setWallet(username:string, balance:number) {
            this.username = username;
            this.balance = balance;
        }
    },
    getters: {
        currentBalance: (state) => state.balance
    }
});