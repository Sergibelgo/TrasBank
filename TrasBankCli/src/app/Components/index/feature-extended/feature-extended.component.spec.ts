import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FeatureExtendedComponent } from './feature-extended.component';

describe('FeatureExtendedComponent', () => {
  let component: FeatureExtendedComponent;
  let fixture: ComponentFixture<FeatureExtendedComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FeatureExtendedComponent]
    });
    fixture = TestBed.createComponent(FeatureExtendedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
