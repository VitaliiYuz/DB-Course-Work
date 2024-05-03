import {makeAutoObservable} from "mobx";

export default class UserStore {
    constructor() {
        this._isAuth = false;
        this._user = {};
        this._basket = {};
        this._basketTotal = 0;
        makeAutoObservable(this);
    }

    setIsAuth(bool){
        this._isAuth = bool;
    }

    setUser(user){
        this._user = user;
    }

    setBasket(basket) {
        this._basket = basket;
    }

    setBasketTotal(total){
        this._basketTotal = total;
    }

    isAdmin() {
        return this._user !== null && this._user.role === 1;
    }

    isOwner(){
        return this._user !== null && this._user.role === 2;
    }

    isClient(){
        return this._user !== null && this._user.role === 0;
    }

    get isAuth(){
        return this._isAuth;
    }

    get user(){
        return this._user;
    }

    get basket(){
        return this._basket;
    }

    get basketTotal(){
        return this._basketTotal;
    }
}