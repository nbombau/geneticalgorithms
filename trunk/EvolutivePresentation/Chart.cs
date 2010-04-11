using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Evolutive;

namespace EvolutivePresentation
{
    /// <summary>
    /// Control de Graficos
    /// </summary>
    public class Chart : System.Windows.Forms.Control
    {
        // Tipos
        public enum SeriesType
        {
            Line,
            Dots
        }

        // Series de datos
        private class DataSeries
        {
            public double[,] data = null;
            public Color color = Color.Blue;
            public SeriesType type = SeriesType.Line;
            public int width = 1;
        }

        // tabla de datos
        Hashtable seriesTable = new Hashtable();

        private Pen blackPen = new Pen(Color.Black);
        private Brush whiteBrush = new SolidBrush(Color.White);

        private DoubleRange rangeX = new DoubleRange(0, 1);
        private DoubleRange rangeY = null;

        /// <summary>
        /// Rango en X 
        /// </summary>
        public DoubleRange RangeX
        {
            get { return rangeX; }
            set
            {
                rangeX = value;
            }
        }

        /// <summary>
        /// Rango en Y
        /// </summary>
        public DoubleRange RangeY
        {
            get { return rangeY; }
            set
            {
                rangeY = value;
            }
        }

        /// <summary>
        /// Variable utilizada por winforms
        /// </summary>
        private System.ComponentModel.Container components = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public Chart()
        {
            InitializeComponent();

            // parametros
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer | ControlStyles.UserPaint, true);
        }

        /// <summary>
        /// Se limpian los recursos
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();

                // se limpian los recursos graficos
                blackPen.Dispose();
                whiteBrush.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }
        #endregion

        // Evento de escritura del control
        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            int clientWidth = ClientRectangle.Width;
            int clientHeight = ClientRectangle.Height;

            // fondo blanco
            g.FillRectangle(whiteBrush, 0, 0, clientWidth - 1, clientHeight - 1);

            // rectangulo negro
            g.DrawRectangle(blackPen, 0, 0, clientWidth - 1, clientHeight - 1);

            // si hay cosas para graficar
            if (rangeY != null)
            {
                double xFactor = (double)(clientWidth - 10) / (rangeX.Length);
                double yFactor = (double)(clientHeight - 10) / (rangeY.Length);

                // se recorren las series a graficar
                IDictionaryEnumerator en = seriesTable.GetEnumerator();
                while (en.MoveNext())
                {
                    DataSeries series = (DataSeries)en.Value;
                    // se obtiene el dato de las series
                    double[,] data = series.data;

                    // se chequea que haya datos
                    if (data == null)
                        continue;

                    // si la serie se grafica en puntos
                    if (series.type == SeriesType.Dots)
                    {
                        // se dibujan todos los puntos
                        Brush brush = new SolidBrush(series.color);
                        int width = series.width;
                        int r = width >> 1;

                        for (int i = 0, n = data.GetLength(0); i < n; i++)
                        {
                            int x = (int)((data[i, 0] - rangeX.Min) * xFactor);
                            int y = (int)((data[i, 1] - rangeY.Min) * yFactor);

                            x += 5;
                            y = clientHeight - 6 - y;

                            g.FillRectangle(brush, x - r, y - r, width, width);
                        }
                        brush.Dispose();
                    }
                    else
                    {
                        // dibujar linea
                        Pen pen = new Pen(series.color, series.width);

                        int x1 = (int)((data[0, 0] - rangeX.Min) * xFactor);
                        int y1 = (int)((data[0, 1] - rangeY.Min) * yFactor);

                        x1 += 5;
                        y1 = clientHeight - 6 - y1;

                        // se dibujan todas las lineas
                        for (int i = 1, n = data.GetLength(0); i < n; i++)
                        {
                            int x2 = (int)((data[i, 0] - rangeX.Min) * xFactor);
                            int y2 = (int)((data[i, 1] - rangeY.Min) * yFactor);

                            x2 += 5;
                            y2 = clientHeight - 6 - y2;

                            g.DrawLine(pen, x1, y1, x2, y2);

                            x1 = x2;
                            y1 = y2;
                        }
                        pen.Dispose();
                    }
                }
            }

            // se llama al evento base
            base.OnPaint(pe);
        }

        /// <summary>
        /// Agregado de una serie al grafico
        /// </summary>
        public void AddDataSeries(string name, Color color, SeriesType type, int width)
        {
            DataSeries series = new DataSeries();
            series.color = color;
            series.type = type;
            series.width = width;
            seriesTable.Add(name, series);
        }

        /// <summary>
        /// Actualizar una serie del grafico
        /// </summary>
        public void UpdateDataSeries(string name, double[,] data)
        {
            DataSeries series = (DataSeries)seriesTable[name];
            series.data = data;

            UpdateYRange();
            Invalidate();
        }

        // Recalcular el rando en Y
        private void UpdateYRange()
        {
            double minY = double.MaxValue;
            double maxY = double.MinValue;

            // recorrer todas las series
            IDictionaryEnumerator en = seriesTable.GetEnumerator();
            while (en.MoveNext())
            {
                DataSeries series = (DataSeries)en.Value;
                // obtener datos de las series
                double[,] data = series.data;

                if (data != null)
                {
                    for (int i = 0, n = data.GetLength(0); i < n; i++)
                    {
                        double v = data[i, 1];
                        // checkear maximo
                        if (v > maxY)
                            maxY = v;
                        // checkear minimo
                        if (v < minY)
                            minY = v;
                    }
                }
            }

            if ((minY != double.MaxValue) || (maxY != double.MinValue))
            {
                rangeY = new DoubleRange(minY, maxY);
            }
        }
    }
}
