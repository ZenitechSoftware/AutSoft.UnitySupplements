# DatePicker [![openupm](https://img.shields.io/npm/v/com.autsoft.unitysupplements.uicomponents?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.autsoft.unitysupplements.uicomponents/)

Date and time selector ui component for Unity. Complete with a calendar-style date picker and spinner-like time picker.

![Calendar](~/images/DatePicker/calendar.jpg)
![Spinners](~/images/DatePicker/time.jpg)

> [!TIP]
> See the [MRTKExtras](~/articles/MRTKExtras/MRTKExtras.md) package for an XR compatible date picker.

## Installation

Use [OpenUPM](https://openupm.com/) to install the package.

```
openupm add com.autsoft.unitysupplements.uicomponents
```

## Getting started

### Add DatePicker to your scene

Use the menu or context menu as with any other component:

![AddingDatePicker](~/images/DatePicker/addingdatepicker.jpg)

Make sure to place it under a  `Canvas` gameobject. You may optionally assign a custom `TMP_Font` asset to the script to customize the look.

### Usage

Get the currently selected `DateTimeOffset`:
```csharp
return _datePicker.PickedDateTime;
```

Bind to date picking event:
```csharp
this.Bind(_datePicker.onDateTimePicked, date => _inputField.text = date.ToString("G"));
```

Set the currently selected date and time programmatically:
```csharp
_datePicker.InitWithDate(DateTimeOffset.Now);
```

See the sample scene for a working example.