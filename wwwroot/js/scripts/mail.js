"use strict";
import { createTransport } from '../../node_modules/nodemailer';

function sendMail() {
  var transporter = createTransport({
    service: 'gmail',
    auth: {
      user: 'rodickova.gabriela@gmail.com',
      pass: '2804982828'
    }
  });

  var mailOptions = {
    from: 'rodickova.gabriela@gmail.com',
    to: 'eaaaaj@students.eaaa.dk',
    subject: 'Your blob is sad',
    text: 'take care of your blob'
  };

  transporter.sendMail(mailOptions, function (error, info) {
    if (error) {
      console.log(error);
    } else {
      console.log('Email sent: ' + info.response);
    }
  });
}

sendMail();