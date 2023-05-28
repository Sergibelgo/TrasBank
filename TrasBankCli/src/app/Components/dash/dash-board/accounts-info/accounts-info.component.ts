import { Component } from '@angular/core';
import { CalendarOptions } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import listPlugin from '@fullcalendar/list';

@Component({
  selector: 'app-accounts-info',
  templateUrl: './accounts-info.component.html',
  styleUrls: ['./accounts-info.component.css']
})
export class AccountsInfoComponent {
  calendarOptions: CalendarOptions = {
    //initialView: 'dayGridMonth',
    //plugins: [dayGridPlugin],
    initialView: 'dayGridMonth',
    plugins: [listPlugin, dayGridPlugin],
    editable:true
  };
  prueba() {
    
  }
}
