import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-feature',
  templateUrl: './feature.component.html',
  styleUrls: ['./feature.component.css']
})
export class FeatureComponent {
  @Input() icon: string | undefined;
  @Input() title: string | undefined;
  @Input() text: string | undefined;
}
