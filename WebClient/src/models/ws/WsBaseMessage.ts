import {MessageType} from '@/models/TypeAlias';

export interface WsBaseMessage {
    messageType: MessageType;
}
