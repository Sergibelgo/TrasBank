import { Message } from "../message/message";

export interface MessagesState {
  messages: Message[];
  notReaded: boolean;
}
