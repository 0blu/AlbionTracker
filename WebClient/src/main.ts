import Vue from 'vue';
import App from './App.vue';
import store from './store';
import WS from './plugins/WsWrapper';
import {PlayerNameAnonymizer} from './libs/PlayerNameAnonymizer';
import {Entity} from '@/models/Entity';
import {GameObjectType} from '@/models/TypeAlias';

Vue.config.productionTip = false;

Vue.filter('getEntityName', function getEntityName(entity: Entity) {
    if (!entity) {
        return '<UNDEFINED>';
    }
    if (store.state.config.anonymizeNames && entity.type === GameObjectType.Player) {
        return PlayerNameAnonymizer.getAnonymizedName(entity);
    }
    return entity.name;
});

new Vue({
    store,
    render: (h) => h(App),
}).$mount('#app');



// WS(store, `ws://${window.location.host}/ws`);
WS(store, `ws://127.0.0.1:4537/ws`);
