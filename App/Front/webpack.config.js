const path = require('path');
const process = require('process');
const webpackConfig = require('webpack');

const {CleanWebpackPlugin} = require('clean-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');

const srcDirectory = path.resolve(__dirname, 'src');
const publicDirectory = path.resolve(__dirname, 'public');
const wwwrootDirectory = path.resolve(__dirname, '../wwwroot/bundle');
const nodeModulesDirectory = path.resolve(__dirname, 'node_modules');


module.exports = {
  entry: './src/entry/plain.tsx',
  output: {
    path: wwwrootDirectory,
    publicPath: '/',
    filename: '[name].bundle.js',
    chunkFilename: '[name].[chunkhash].chunk.js',
  },
  resolve: {
    extensions: ['.tsx', '.ts', '.js'],
    modules: [nodeModulesDirectory, srcDirectory],
  },
  module: {
    rules: [
      {
        test: /\.(tsx|ts)$/,
        exclude: [nodeModulesDirectory],
        use: ['babel-loader', 'ts-loader'],
        
      },
      {
        test: /\.s[ca]ss$/,
        include: srcDirectory,
        exclude: nodeModulesDirectory,
        use: [
          ...cssLoaders(),
          {
            loader: 'sass-loader',
            options: {
              sassOptions: {
                includePaths: [srcDirectory],
              },
            },
          },
        ],
      },
      {
        test: /\.(png|gif)$/,
        include: srcDirectory,
        use: ['file-loader'],
      },
      { 
        test: /\.css$/, 
        use: [ 'style-loader', 'css-loader' ] 
      }
    ],
  },
  plugins: [
    new webpackConfig.DefinePlugin({
      'process.env.NODE_ENV' : JSON.stringify('development')
    }),
    new webpackConfig.ProvidePlugin({
      process: 'process/browser',
    }),
    new CleanWebpackPlugin(),
    new MiniCssExtractPlugin({
      filename: '[name].bundle.css',
      ignoreOrder: true,
      chunkFilename: '[name].[chunkhash].chunk.css',
    }),
    new webpackConfig.HotModuleReplacementPlugin(),
  ],
  target: ['web', 'es5'],
  mode: 'development'
};

function cssLoaders() {
  return [
    MiniCssExtractPlugin.loader,
    {
      loader: 'css-loader',
      options: {
        modules: true,
      },
    },
    'postcss-loader',
  ];
}
