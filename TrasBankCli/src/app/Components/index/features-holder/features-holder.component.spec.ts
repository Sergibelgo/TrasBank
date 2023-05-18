import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FeaturesHolderComponent } from './features-holder.component';

describe('FeaturesHolderComponent', () => {
  let component: FeaturesHolderComponent;
  let fixture: ComponentFixture<FeaturesHolderComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FeaturesHolderComponent]
    });
    fixture = TestBed.createComponent(FeaturesHolderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
