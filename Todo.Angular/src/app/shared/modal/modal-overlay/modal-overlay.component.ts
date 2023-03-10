import { Component, OnInit } from '@angular/core';
import { ModalService } from 'src/app/services/modal.service';

@Component({
  selector: 'app-modal-overlay',
  templateUrl: './modal-overlay.component.html',
  styleUrls: ['./modal-overlay.component.scss'],
})
export class ModalOverlayComponent {
  constructor(private modalService: ModalService) {}

  onOverlayClick() {
    this.modalService.modalIsVisibleChanged.next(false);
  }
}
