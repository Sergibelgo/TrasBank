import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalTransactionComponent } from './modal-transaction.component';

describe('ModalTransactionComponent', () => {
  let component: ModalTransactionComponent;
  let fixture: ComponentFixture<ModalTransactionComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ModalTransactionComponent]
    });
    fixture = TestBed.createComponent(ModalTransactionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
