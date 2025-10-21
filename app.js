const express = require('express');
const fs = require('fs');

const app = express();
app.use(express.json());

app.post('/dane', (req, res) => {
  const data = req.body;

  if (!Array.isArray(data) || data.length === 0) {
    return res.status(400).json({ error: 'Brak danych' });
  }

  let csv = 'X,Y\n';
  data.forEach(item => {
    csv += `${item.X},${item.Y}\n`;
  });

  fs.writeFile('dane.csv', csv, (err) => {
    if (err) return res.status(500).json({ error: 'Błąd zapisu pliku' });
    res.json({ success: true });
  });
});

app.listen(3000, () => {
  console.log('Serwer działa na porcie 3000');
});
