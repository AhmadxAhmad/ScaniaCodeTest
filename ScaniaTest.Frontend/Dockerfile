FROM node:latest

WORKDIR /usr/src/app

COPY package*.json ./

RUN npm install -g @angular/cli
RUN npm install

COPY . .

RUN ng build 

EXPOSE 80

CMD ["ng", "serve", "--host", "0.0.0.0", "--port", "80"]
