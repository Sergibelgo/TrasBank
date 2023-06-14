import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateAccountNameModalComponent } from './update-account-name-modal.component';

describe('UpdateAccountNameModalComponent', () => {
  let component: UpdateAccountNameModalComponent;
  let fixture: ComponentFixture<UpdateAccountNameModalComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UpdateAccountNameModalComponent]
    });
    fixture = TestBed.createComponent(UpdateAccountNameModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
