FROM node:lts-alpine
WORKDIR /Cine-front
COPY package.json package-lock.json ./
RUN npm install --production
COPY . . 
EXPOSE 3000
CMD ["node", "cine_web_app/front-end/js/server.js"]