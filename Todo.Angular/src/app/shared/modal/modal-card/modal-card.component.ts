import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-modal-card',
  templateUrl: './modal-card.component.html',
  styleUrls: ['./modal-card.component.scss'],
})
export class ModalCardComponent {
  @Input() title: string;
  @Input() isEdit: boolean;
  @Output() onClose = new EventEmitter<any>();
  @Output() onCancel = new EventEmitter<any>();
  @Output() onAdd = new EventEmitter<any>();

  onCloseClick() {
    this.onClose.emit();
  }

  onCancelClick() {
    this.onCancel.emit();
  }

  onAddClick() {
    this.onAdd.emit();
  }
}
