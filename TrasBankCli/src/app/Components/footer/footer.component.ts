/**
 * Footer component, is used on every page and displays all common information and language changes
 * Right now is been used on app module so is no necesary to repeat it
 * It should go as the last component or at least as the last block of dom
 * Example usage:
 * <app-router></...>
 * <app-footer></...>
 */
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent {
  constructor(private translate: TranslateService,private router:Router) {
  }
  useLanguage(language: string): void {
    this.translate.use(language);
  }
  goToWL() {
    this.router.navigate(["loginWorker"]);
  }
}
