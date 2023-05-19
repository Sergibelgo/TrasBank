import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-feature-extended',
  templateUrl: './feature-extended.component.html',
  styleUrls: ['./feature-extended.component.css']
})
export class FeatureExtendedComponent {
  @Input() src: string | undefined;
  @Input() title: string | undefined;
  @Input() text: string | undefined;
}
