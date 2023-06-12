import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoansManagerComponent } from './loans-manager.component';

describe('LoansManagerComponent', () => {
  let component: LoansManagerComponent;
  let fixture: ComponentFixture<LoansManagerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LoansManagerComponent]
    });
    fixture = TestBed.createComponent(LoansManagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
