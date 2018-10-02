import {Store} from 'vuex';
import {WsBaseMessage} from '@/models/ws/WsBaseMessage';
import {AlbionDamageTrackerStoreState} from '@/store';

type MessageListener = (data: WsBaseMessage) => void;

export class WsWrapper {
    private ws?: WebSocket;

    public constructor(private url: string, private messageListener: MessageListener) {
        this.init();
    }

    private init() {
        if (this.ws) {
            this.ws.onmessage = null;
            this.ws.onclose = null;
            this.ws.onopen = null;
            this.ws.close();
        }

        this.ws = new WebSocket(this.url);

        this.ws.onmessage = this.onMessage.bind(this);
        this.ws.onclose = this.init.bind(this);
        this.ws.onopen = this.onOpen.bind(this);
    }

    private onMessage(event: MessageEvent) {
        this.messageListener(JSON.parse(event.data));
    }

    private onOpen() {
        console.log(this.ws, 'Connected :)');
    }
}

export default function wsWrapperInit(store: Store<AlbionDamageTrackerStoreState>, url: string): WsWrapper {
    return new WsWrapper(url, (message) => {
        store.dispatch(`ws${message.messageType}`, message);
    });
}
