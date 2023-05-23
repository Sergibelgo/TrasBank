export class Transaction {
  type: string;
  ammount: number;
  date: Date;
  other: string;
  concep: string;
  constructor(type: string, ammount: number, date: Date, other: string, concep: string) {
    this.type = type;
    this.ammount = ammount;
    this.date = date;
    this.other = other;
    this.concep = concep;
  }
}
